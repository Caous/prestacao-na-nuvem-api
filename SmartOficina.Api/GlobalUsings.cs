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


//API
global using SmartOficina.Api.Dto;
global using SmartOficina.Api.Filters;

//Domain
global using SmartOficina.Api.Domain.Model;

//Infrastructure
global using SmartOficina.Api.Infrastructure.Context;
global using SmartOficina.Api.Infrastructure.Repositories.GenericRepository;
global using SmartOficina.Api.Infrastructure.Repositories.Interfaces;
global using SmartOficina.Api.Infrastructure.Repositories.Services;
global using SmartOficina.Api.Infrastructure.Configurations.ContextConfiguration;
global using SmartOficina.Api.Infrastructure.Configurations.DependecyInjectionConfig;
global using SmartOficina.Api.Infrastructure.Configurations.SwaggerConfig;
global using SmartOficina.Api.Infrastructure.Constants;

