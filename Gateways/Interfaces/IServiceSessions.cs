
using TeamHubSessionsServices.DTOs;
using TeamHubSessionsServices.Entities;

namespace TeamHubSessionsServices.Gateways.Interfaces;

public interface IServicesSessions {
    public studentsession SearchLastSession(student student);
    public studentsession CreateSession(student student, string token, string ip);
}