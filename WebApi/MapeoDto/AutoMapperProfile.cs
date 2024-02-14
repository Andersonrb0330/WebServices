using AutoMapper;
using WebApi.Dominio;
using WebApi.Dtos.RequestDtos;
using WebApi.Dtos.ResponseDtos;

namespace WebApi.MapeoDto
{
    public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<ProductoParametroDto, Producto>();

            CreateMap<Pais, PaisDto>().ReverseMap();
            CreateMap<PaisParametroDto, Pais>();

            CreateMap<TipoProducto,TipoProductoDto>().ReverseMap();
            CreateMap<TipoProductoParametroDto, TipoProducto>();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<UsuarioParametroDto, Usuario>();
        }
	}
}

