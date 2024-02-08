//Dependency Nuget
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.ValueGeneration;
global using Microsoft.AspNetCore.Mvc.Filters;
global using System.Runtime.InteropServices;
global using AutoMapper;
global using System.Security.Cryptography;
global using Microsoft.OpenApi.Models;
global using System.Text.Json.Serialization;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using System.Reflection;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.EntityFrameworkCore.Design;
global using System.Text.Json;
global using System.IdentityModel.Tokens.Jwt;


//API
global using PrestacaoNuvem.Api.Dto;
global using PrestacaoNuvem.Api.Converters;
global using PrestacaoNuvem.Api.Util;

//Domain
global using PrestacaoNuvem.Api.Domain.Model;
global using PrestacaoNuvem.Api.Domain.Interfaces;
global using PrestacaoNuvem.Api.Domain.Services;


//Infrastructure
global using PrestacaoNuvem.Api.Infrastructure.Context;
global using PrestacaoNuvem.Api.Infrastructure.Repositories.GenericRepository;
global using PrestacaoNuvem.Api.Infrastructure.Repositories.Interfaces;
global using PrestacaoNuvem.Api.Infrastructure.Repositories.Services;
global using PrestacaoNuvem.Api.Infrastructure.Configurations.ContextConfiguration;
global using PrestacaoNuvem.Api.Infrastructure.Configurations.DependecyInjectionConfig;
global using PrestacaoNuvem.Api.Infrastructure.Configurations.SwaggerConfig;
global using PrestacaoNuvem.Api.Infrastructure.Constants;
global using PrestacaoNuvem.Api.Infrastructure.Configurations.Security;

