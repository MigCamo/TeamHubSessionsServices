
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;
using TeamHubSessionsServices.Gateways.Interfaces;

namespace TeamHubSessionsServices.Gateways.Providers;

class ServiceSessionsMySQL : IServicesSessions
{
    private TeamHubContext dbContext;

    public ServiceSessionsMySQL(TeamHubContext dbContext) {
        this.dbContext = dbContext;
    }

    public studentsession CreateSession(student student, string token, string ip)
    {
        studentsession studentSession = new studentsession() {
            IdStudent = student.IdStudent,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            IPAdress = ip,
            Token = token
        };
        dbContext.studentsession.Add(studentSession);
        dbContext.SaveChanges();
        return studentSession;
    }

    public studentsession? SearchLastSession(student student)
    {
        var studentSession = dbContext.studentsession.Where(us => us.IdStudent == student.IdStudent)
                    .OrderByDescending(us => us.StartDate)
                    .FirstOrDefault();
        return studentSession; 
    }
}
