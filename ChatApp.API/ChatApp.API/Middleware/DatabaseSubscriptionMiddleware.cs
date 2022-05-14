using ChatApp.Core.Interface;
using Microsoft.AspNetCore.Builder;

namespace ChatApp.API.Middleware
{
    public static class DatabaseSubscriptionMiddleware
    {
        public static void UseDataSubscription<T>(this IApplicationBuilder builder, string tableName) where T : class, IDatabaseSubscription
        {
            var subscription = (T)builder.ApplicationServices.GetService(typeof(T));
            subscription.Configure(tableName);
        }
    }
}
