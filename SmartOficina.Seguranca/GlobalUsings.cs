//Package Nuggets
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using AutoMapper;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Principal;
global using Microsoft.OpenApi.Models;

//SmartOficina
global using SmartOficina.Seguranca.Infrastructure.Configurations.Security;
global using SmartOficina.Seguranca.Mappers;
global using SmartOficina.Seguranca.Domain.Model;
global using SmartOficina.Seguranca.Infrastructure.Context;
global using SmartOficina.Seguranca.Dto;
global using SmartOficina.Seguranca.Infrastructure.Configurations.Repositories.Interfaces;
global using SmartOficina.Seguranca.Infrastructure.Repositories.Services;
global using SmartOficina.Seguranca.Infrastructure.Configurations.ContextConfiguration;
global using SmartOficina.Seguranca.Infrastructure.Configurations.DependecyInjectionConfig;
global using SmartOficina.Seguranca.Infrastructure.Configurations.SwaggerConfig;
