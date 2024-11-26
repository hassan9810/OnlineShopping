namespace Store.Extensions
{
    public static class ServiceExtension
    {
        public static void AddStoreServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ExceptionMW>();
            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(ResponseHelper));
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter a valid token!",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {      
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                        },
                        new string[]{}
                    }
                });
            });
        }
        public static void AddDbStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("default"));
            });
        }

        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<StoreContext>();

            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var authKey = new SymmetricSecurityKey(key);
            services.AddAuthentication(opt =>
            {
                // this to make default authorization schema is Bearer so in your endpoint no need to do  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                // only use [Authorize]
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    // here we validate the user token against  jwt token 
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authKey
                    };
                });

        }
        public static void AddCorsOrigin(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
