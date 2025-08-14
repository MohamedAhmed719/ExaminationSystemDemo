namespace ExaminationSystemDemo.Contracts.Authentiaction;

public record StudentRegisterRequest(string FirstName,string LastName,string Email,string Password, int Grade);
