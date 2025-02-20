using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WFKeevo.Data;
using WFKeevo.Services;
using WFKeevo.Models;
using static WFKeevo.Models.MntLancto;
using System;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WFKeevo.Modulo
{
    public class MntLancto
    {
        private readonly WFKeevoDBContext _context;
        public MntLancto(WFKeevoDBContext context)
        {
            _context = context;
        }

        public PaginacaoResponse<ConsultaLanctoReponse> ConsultaLancto([FromQuery] ConsultaLanctoRequest request, int skip, int take, bool ordemDesc)
        {
            request.TarCodigo = request.TarCodigo == null ? 0 : request.TarCodigo;

            var consultaLanctoReponse = (from l in _context.Lancto
                                         join u in _context.Usuario on l.UsuarioId equals u.Id
                                         join t in _context.Tarefa on l.TarCodigo equals t.TarCodigo
                                         where
                                         (request.TarCodigo == 0 || l.TarCodigo == request.TarCodigo) &&
                                         (request.TarNome != null && t.TarNome.ToUpper().Contains(request.TarNome.ToUpper())) &&
                                         (request.UsuarioLogin != null && u.Login.ToUpper().Contains(request.UsuarioLogin.ToUpper())) &&
                                         (request.UsuarioNome != null && u.Nome.ToUpper().Contains(request.UsuarioNome.ToUpper())) &&
                                         ((request.DataInicial != null && request.DataFinal != null) && (l.LanDataInicio >= request.DataInicial && l.LanDataInicio <= request.DataFinal)) &&
                                         ((request.HorarioAberto == 'N' && l.LanDataFinal != null) ||
                                         (request.HorarioAberto == 'S' && l.LanDataFinal == null)) &&
                                         (request.UsuarioFuncao != null && u.Funcao.ToUpper().Contains(request.UsuarioFuncao.ToUpper()))

                                         select new ConsultaLanctoReponse
                                         {
                                             LanId = l.LanId ?? Guid.Empty,
                                             LanDataInicial = DateTime.SpecifyKind(l.LanDataInicio, DateTimeKind.Utc),
                                             LanDataFinal = l.LanDataFinal.HasValue ? (DateTime?)DateTime.SpecifyKind(l.LanDataFinal.Value, DateTimeKind.Utc) : null,
                                             TarCodigo = t.TarCodigo,
                                             TarNome = t.TarNome,
                                             Id = u.Id,
                                             Login = u.Login,
                                             Nome = u.Nome,
                                             Funcao = u.Funcao,
                                         });

            var consultaOrdenada = ordemDesc
                ? consultaLanctoReponse.OrderByDescending(u => u.Nome).OrderBy(l => l.LanDataInicial)
                : consultaLanctoReponse.OrderBy(u => u.Nome).ThenBy(l => l.LanDataInicial);

            long qtde = consultaOrdenada.Count();

            var lista = consultaOrdenada
                .Skip(((skip - 1) * take))
                .Take(take)
                .ToList();

            var paginacaoResponse = new PaginacaoResponse<ConsultaLanctoReponse>(lista, qtde, skip, take);

            return paginacaoResponse;
        }

        public ConsisteLanctoResponse ConsisteLancto(ConsisteLanctoResquest request)
        {
            ConsisteLanctoResponse consisteLanctoResponse = new ConsisteLanctoResponse();

            consisteLanctoResponse.MensagemErro = null;

            if (request.LanDataInicial == request.LanDataFinal)
            {
                consisteLanctoResponse.MensagemErro = "A data inicial não pode ser igual a data final.";
                return consisteLanctoResponse;
            }

            if (request.LanDataInicial > request.LanDataFinal)
            {
                consisteLanctoResponse.MensagemErro = "A data inicial não pode ser maior a data final.";
                return consisteLanctoResponse;
            }

            if (request.LanDataFinal.HasValue && request.LanDataInicial.Date != request.LanDataFinal.Value.Date)
            {
                consisteLanctoResponse.MensagemErro = "O lançamento de horas tem que ser no mesmo dia.";
                return consisteLanctoResponse;
            }

            var lanctos = _context.Lancto
                .Where(l => l.LanDataFinal == null && l.UsuarioId == request.Id)
                .Select(l => new Lancto
                {
                    LanId = l.LanId ?? Guid.Empty,
                    LanDataInicio = DateTime.SpecifyKind(l.LanDataInicio, DateTimeKind.Utc),
                    LanDataFinal = l.LanDataFinal.HasValue ? DateTime.SpecifyKind(l.LanDataFinal.Value, DateTimeKind.Utc) : (DateTime?)null,
                    TarCodigo = l.TarCodigo,
                    UsuarioId = l.UsuarioId,
                })
                .ToList();

            if (lanctos.Any() && request.InclusaoRegistro)
            {
                consisteLanctoResponse.MensagemErro = "Existe lançamento em aberto.";
                return consisteLanctoResponse;
            }

            lanctos = new List<Lancto>();
            lanctos = _context.Lancto
                .Where(l => (l.LanDataInicio.Year == request.LanDataInicial.Year &&
                             l.LanDataInicio.Month == request.LanDataInicial.Month &&
                             l.LanDataInicio.Day == request.LanDataInicial.Day &&
                             l.LanDataInicio.Hour == request.LanDataInicial.Hour &&
                             l.LanDataInicio.Minute == request.LanDataInicial.Minute) &&
                            l.UsuarioId == request.Id)
                .Select(l => new Lancto
                {
                    LanId = l.LanId ?? Guid.Empty,
                    LanDataInicio = DateTime.SpecifyKind(l.LanDataInicio, DateTimeKind.Utc),
                    LanDataFinal = l.LanDataFinal.HasValue ? DateTime.SpecifyKind(l.LanDataFinal.Value, DateTimeKind.Utc) : (DateTime?)null,
                    TarCodigo = l.TarCodigo,
                    UsuarioId = l.UsuarioId,
                })
                .ToList();

            if (lanctos.Any())
            {
                if (request.InclusaoRegistro)
                {
                    consisteLanctoResponse.MensagemErro = "Existe lançamento com mesmo horário de início informado.";
                    return consisteLanctoResponse;
                }
            }

            // Se a data final for nula não fará verificação de horários sobrepostos
            if (!request.LanDataFinal.HasValue)
            {
                return consisteLanctoResponse;
            }
                
            List<ArrayDeHora> arrayDeHoras = new List<ArrayDeHora>();

            arrayDeHoras = _context.Lancto
                .Where(l => l.UsuarioId == request.Id &&
                            l.LanDataInicio.Day == request.LanDataInicial.Day)
                .Select(l => new ArrayDeHora
                {
                    HoraFim = l.LanDataFinal == null ?
                                            int.Parse($"{((DateTime)request.LanDataFinal).Hour:D2}{((DateTime)request.LanDataFinal).Minute:D2}") 
                                            : 
                                            int.Parse($"{((DateTime)l.LanDataFinal).Hour:D2}{((DateTime)l.LanDataFinal).Minute:D2}"),
                    HoraIni = int.Parse($"{l.LanDataInicio.Hour:D2}{l.LanDataInicio.Minute:D2}"),
                })
                .ToList();

            if (arrayDeHoras.Count == 1 || arrayDeHoras.Count == 0) 
            {
                return consisteLanctoResponse;
            }

            if (arrayDeHoras != null)
            {
                int hrIniRequest = int.Parse($"{request.LanDataInicial.Hour:D2}{request.LanDataInicial.Minute:D2}");
                int hrFimRequest = int.Parse($"{((DateTime)request.LanDataFinal).Hour:D2}{((DateTime)request.LanDataFinal).Minute:D2}");

                // Conversão do array de volta para uma lista para adicionar um novo elemento
                List<ArrayDeHora> listaArray = arrayDeHoras.ToList();

                // Adicionar a hora inicial e final do request no array
                listaArray.Add(new ArrayDeHora(hrIniRequest, hrFimRequest));

                // Order by HoraIni in ascending order
                listaArray = listaArray.OrderBy(x => x.HoraIni).ToList();

                // Transformar de volta para array
                ArrayDeHora[] arrayFinal = listaArray.ToArray();

                // Procurar pelo hrIniRequest e hrFimRequest para saber a posição que ficou
                // a hrFimRequest tem que ser maior que seu anterior e menor que seu sucessor
                // Tomar cuidado quando for o primeiro da lista ou ultimo da lista
                int posicaoHoraRequest = 0;

                for (global::System.Int32 i = 0; i < arrayFinal.Length; i++)
                {
                    if ((hrIniRequest == arrayFinal[i].HoraIni) &&
                        (hrFimRequest == arrayFinal[i].HoraFim))
                    {
                        posicaoHoraRequest = i;
                        break;
                    }
                }

                if ((posicaoHoraRequest == 0) &&
                    ((arrayFinal[posicaoHoraRequest].HoraFim > arrayFinal[posicaoHoraRequest + 1].HoraFim)))
                {
                    consisteLanctoResponse.MensagemErro = $"Existe lançamento com sobreposição de horário: {arrayFinal[posicaoHoraRequest + 1].HoraFim.ToString()}";
                }
                else
                {
                    if ((posicaoHoraRequest == arrayFinal.Length - 1) &&
                        (arrayFinal[posicaoHoraRequest].HoraFim < arrayFinal[posicaoHoraRequest - 1].HoraFim))
                    {
                        consisteLanctoResponse.MensagemErro = $"Existe lançamento com sobreposição de horário: {arrayFinal[posicaoHoraRequest - 1].HoraFim.ToString()}";

                    } else
                    {
                        if ((posicaoHoraRequest > 0 && posicaoHoraRequest < arrayFinal.Length - 1) &&
                            ((arrayFinal[posicaoHoraRequest].HoraFim < arrayFinal[posicaoHoraRequest - 1].HoraFim) ||
                             (arrayFinal[posicaoHoraRequest].HoraFim > arrayFinal[posicaoHoraRequest + 1].HoraFim)))
                        {
                            consisteLanctoResponse.MensagemErro = $"Existe lançamento com sobreposição de horário: {arrayFinal[posicaoHoraRequest - 1].HoraFim.ToString()} ou {arrayFinal[posicaoHoraRequest + 1].HoraFim.ToString()}";
                        }
                    }
                }
            }

            return consisteLanctoResponse;
        }
    }
}