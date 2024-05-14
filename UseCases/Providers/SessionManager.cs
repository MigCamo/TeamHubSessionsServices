
using AppServiciosIdentidad.Gateways.Interfaces;
using TeamHubSessionsServices.Entities;
using TeamHubSessionsServices.Gateways.Interfaces;
using TeamHubSessionsServices.UseCases.Interfaces;

namespace TeamHubSessionsServices.UseCases.Provider;

public class SessionManager : ISessionManager
{
    private IServicesUsers ServicesUsers;
    private IServicesSessions ServiceSessions;
    private IConfiguration config;
    private ITokenGenerator TokenGenerator;

    public SessionManager(IServicesUsers servicesUsers,
                           IServicesSessions serviceSessions, 
                           ITokenGenerator tokenGenerator) {
        this.ServicesUsers = servicesUsers;
        this.ServiceSessions = serviceSessions;
        this.config = config;
        this.TokenGenerator = tokenGenerator;
    }

    public studentsession CreateSesion(student student, string ip, int numberHours)
    {
        var studentSession = SearchCurrentSession(student, numberHours);
        if (studentSession == null) {
            string token = TokenGenerator.GenerateToken(student, numberHours);
            studentSession = ServiceSessions.CreateSession(student, token, ip);
        }
        return studentSession;
    }

    public studentsession SearchCurrentSession(student student, int numberHours)
    {
        var studentSession = ServiceSessions.SearchLastSession(student);   
        if (studentSession != null && 
            studentSession.StartDate != null && 
            DateTime.Now.Subtract(studentSession.StartDate).TotalHours < numberHours)
                return studentSession;
        else
            return null;
    }
}