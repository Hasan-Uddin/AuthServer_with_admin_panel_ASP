namespace Web.Api.Endpoints;

public static class ApiRoutes
{
    private const string _Prefix = "api/";
    private const string _Login = "/signin";
    private const string _Token = "/token";
    private const string _Me = "/me";
    private const string _Create = "";
    private const string _Register = "/signup";
    private const string _GetAll = "";
    private const string _GetById = "/{id:guid}";
    private const string _Update = "/{id:guid}";
    private const string _Delete = "/{id:guid}";

    private static string Path(string resource, string suffix)
        => _Prefix + resource + suffix;

    public static string Login(string resource) => Path(resource, _Login);
    public static string Token(string resource) => Path(resource, _Token);
    public static string Me(string resource) => Path(resource, _Me);
    public static string Register(string resource) => Path(resource, _Register);
    public static string Create(string resource) => Path(resource, _Create);
    public static string GetAll(string resource) => Path(resource, _GetAll);
    public static string GetById(string resource) => Path(resource, _GetById);
    public static string Update(string resource) => Path(resource, _Update);
    public static string Delete(string resource) => Path(resource, _Delete);

   
}
public static class Base
{
    public const string User = "user"; // login, reg
    public const string Users = "users";
    public const string UserProfile = "user-profile";
    public const string UserLoginHistory = "user-login-history";
    public const string Projects = "projects";
    public const string Connect = "connect";
}

//public abstract class Paths
//{
//    protected const string _Prefix = "api/";
//    protected const string _Login = "/signin";
//    protected const string _Create = "/create";
//    protected const string _Register = "/signup";
//    protected const string _GetAll = "/get-all";
//    protected const string _GetById = "/get/{id:guid}";
//    protected const string _Update = "/update/{id:guid}";
//    protected const string _Delete = "/delete/{id:guid}";
//}
//public sealed class ApiRoutes : Paths
//{
//    private readonly string _Base;
//    public ApiRoutes(string B)
//    {
//        _Base = B;
//    }
//    public string Register => _Prefix + _Base + _Register;
//    public string Login => _Prefix + _Base + _Login;
//    public string GetAll => _Prefix + _Base + _GetAll;
//    public string GetById => _Prefix + _Base + _GetById;
//    public string Update => _Prefix + _Base + _Update;
//    public string Delete => _Prefix + _Base + _Delete;
//}
