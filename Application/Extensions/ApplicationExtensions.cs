using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extension;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection service)
    {
        service.AddSingleton<IAppointmentService, AppointmentService>();
        service.AddSingleton<IDayService, DayService>();
        service.AddSingleton<IAuthenticationService, AuthenticationService>();
        service.AddSingleton<IKycService, KycService>();
        service.AddSingleton<IPatientService, PatientService>();
        service.AddSingleton<IPractitionerService, PractitionerService>();

        service.AddSingleton<IMediator, Mediator>();
        return service;
    }
}