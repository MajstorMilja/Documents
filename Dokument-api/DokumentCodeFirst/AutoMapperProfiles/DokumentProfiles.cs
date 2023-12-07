using AutoMapper;
using DokumentCodeFirst.Entity;
using DokumentCodeFirst.Models;

namespace DokumentCodeFirst.AutoMapperProfiles
{
    public class DokumentProfiles : Profile
    {
        public DokumentProfiles() 
        {
            CreateMap<Dokument, DokumentDto>();
            CreateMap<DokumentDto, Dokument>();

            CreateMap<TipDokumenta, TipDokumentaDto>();
            CreateMap<TipDokumentaDto, TipDokumenta>();

            CreateMap<Klijent, KlijentDto>();
            CreateMap<KlijentDto, Klijent>();

            CreateMap<StavkeDokumenta,StavkeDokumentaDto>();
            CreateMap<StavkeDokumentaDto, StavkeDokumenta>();

            CreateMap<Proizvod, ProizvodDto>();
            CreateMap<ProizvodDto, Proizvod>();
        }
    }
}
