using Application.Commands;
using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extension;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection service)
    {
        service.AddScoped<IAppointmentService, AppointmentService>();
        service.AddScoped<IDayService, DayService>();
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IKycService, KycService>();
        service.AddScoped<IPatientService, PatientService>();
        service.AddScoped<IPractitionerService, PractitionerService>();

        service.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<CreateAppointmentHandler>());
        return service;
    }
}