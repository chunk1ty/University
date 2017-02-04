namespace DesignPatterns.Patterns.Factory
{
    using System.Web.Mvc;

    public interface IRedirect
    {
        ActionResult Redirect(int status);
    }
}
