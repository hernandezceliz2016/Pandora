using AutoMapper;
using Business_Logic.Util.funciones;
using Data_Access_Logic.CONTEXT_SOLARIS;
using Entity_Logic.Entity;
using Entity_Logic.Util;

namespace Business_Logic.Util.autoMaper
{
    internal static class clsAutoMaper
    {
        //  private static bool inicalizado;

        public static MapperConfiguration InicializarAutoMaper()
        {
            //if (inicalizado)
            //{
            //    return;
            //}

            var config = new MapperConfiguration(mc =>
            {
                mc.CreateMap<clsUsuario, usuario>()
                    .ForMember(x => x.usuariodocumento, y => y.Ignore());
                mc.CreateMap<clsUsuarioDocumento, usuariodocumento>()
                .ForMember(x => x.usuario, y => y.Ignore());
                mc.CreateMap<usuario, clsUsuario>();
                mc.CreateMap<UploadedFile, uploadfile>();


            });

            //Mapper.Initialize(mc =>
            //{
            //    mc.AddProfile(new clsUsuario());
            //});

            config.AssertConfigurationIsValid();
            //inicalizado = true;
            return config;
        }

    }

    public static class mapear
    {
        public static usuario AutoMapToUsuario(clsUsuario clsUsuario)
        {
            return clsAutoMaper.InicializarAutoMaper().CreateMapper().Map<clsUsuario, usuario>(clsUsuario);
        }

        public static clsUsuario AutoMapToClsUsuario(usuario usuario)
        {
            //  return Mapper.Map<usuario, clsUsuario>(usuario);
            return clsAutoMaper.InicializarAutoMaper().CreateMapper().Map<usuario, clsUsuario>(usuario);
        }

        public static uploadfile AutoMapTouploadfile(UploadedFile objeto)
        {
            return clsAutoMaper.InicializarAutoMaper().CreateMapper().Map<UploadedFile, uploadfile>(objeto);
        }

        public static clsUsuarioDocumento AutoMapToClsUsuarioDocumento(usuariodocumento usuariodocumento)
        {
            return Mapper.Map<usuariodocumento, clsUsuarioDocumento>(usuariodocumento);
        }

        public static usuariodocumento AutoMapToUsuariodocumento(clsUsuarioDocumento clsUsuarioDocumento)
        {
            return Mapper.Map<clsUsuarioDocumento, usuariodocumento>(clsUsuarioDocumento);
        }

    }
}
