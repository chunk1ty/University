using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace VoteSystem.Services.Identity
{
    // TODO do i really need IIdentityMessageService ? try with my custom interface IEmailService
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        //public Task SendAddedParticipantsAsync(List<string> users, Data.Entities.VoteSystem rateSystem)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task SendFeedbackEmailAsync(FeedbackViewModel model)
        //{
        //    throw new System.NotImplementedException();
        //}
    }

    // TODO refactor email service
    //public class EmailService : IIdentityMessageService
    //{
    //    public async Task SendAsync(IdentityMessage message)
    //    {
    //        await ConfigSendGridasync(message);
    //    }

    //    public async Task SendFeedbackEmailAsync(FeedbackViewModel message)
    //    {
    //        await FeedbackEmailAsync(message);
    //    }

    //    public async Task SendAddedParticipantsAsync(List<string> participants, Data.Models.Survey rateSystem)
    //    {
    //        await AddedParticipantsAsync(participants, rateSystem);
    //    }

    //    private async Task FeedbackEmailAsync(FeedbackViewModel message)
    //    {
    //        var myMessage = new SendGridMessage();

    //        myMessage.AddTo("andriyan.krastev@gmail.com");
    //        myMessage.From = new System.Net.Mail.MailAddress(message.Email, message.Name);
    //        myMessage.Subject = message.Subject;
    //        myMessage.Html = message.Message;

    //        var transportWeb = new SendGrid.Web("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");

    //        try
    //        {
    //            if (transportWeb != null)
    //            {
    //                await transportWeb.DeliverAsync(myMessage);
    //            }
    //            else
    //            {
    //                await Task.FromResult(0);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    private async Task ConfigSendGridasync(IdentityMessage message)
    //    {
    //        var myMessage = new SendGridMessage();

    //        myMessage.AddTo(message.Destination);
    //        myMessage.From = new System.Net.Mail.MailAddress("andriyan.krastev@gmail.com", "Система за гласуване.");
    //        myMessage.Subject = message.Subject;
    //        myMessage.Html = message.Body;

    //        var transportWeb = new SendGrid.Web("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");
    //        try
    //        {
    //            if (transportWeb != null)
    //            {
    //                await transportWeb.DeliverAsync(myMessage);
    //            }
    //            else
    //            {
    //                await Task.FromResult(0);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    private async Task AddedParticipantsAsync(List<string> participants, Data.Models.Survey rateSystem)
    //    {
    //        var myMessage = new SendGridMessage();

    //        foreach (var participant in participants)
    //        {
    //            myMessage.AddTo(participant);
    //            myMessage.From = new System.Net.Mail.MailAddress("andriyan.krastev@gmail.com", "Система за гласуване.");
    //            myMessage.Subject = "Начало на система.";
    //            myMessage.Html = "Здравейте! Вие бяхте добавен към "+ rateSystem.Name + " система, която започва на "+ rateSystem.StarDateTime + " и свършва на " + rateSystem.EndDateTime + ". Моля отделете време и гласувайте. " +
    //                "<a href =\"http://votesystem.apphb.com\">Вход към сайта.</a>";

    //            var transportWeb = new SendGrid.Web("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");

    //            try
    //            {
    //                if (transportWeb != null)
    //                {
    //                    await transportWeb.DeliverAsync(myMessage);
    //                }
    //                else
    //                {
    //                    await Task.FromResult(0);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }
    //}
}