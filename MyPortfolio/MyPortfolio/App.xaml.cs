using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using MyPortfolio.Models;

namespace MyPortfolio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use consumers
        //   - for Microsoft Personal account, use consumers
        private static string ClientId = "d9a4b565-d584-4006-a024-a8da752a2d58";

        // Note: Tenant is important for the quickstart.
        private static string Tenant = "consumers";
        private static string Instance = "https://login.microsoftonline.com/";
        private static IPublicClientApplication _clientApp;

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }

        static App()
        {
            CreateApplication();
        }

        public static void CreateApplication()
        {
            BrokerOptions brokerOptions = new BrokerOptions(BrokerOptions.OperatingSystems.Windows);

            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                .WithBroker(brokerOptions)
                .Build();

            MsalCacheHelper cacheHelper = CreateCacheHelperAsync().GetAwaiter().GetResult();

            // Let the cache helper handle MSAL's cache, otherwise the user will be prompted to sign-in every time.
            cacheHelper.RegisterCache(_clientApp.UserTokenCache);
        }

        public static void ShutAppDown()
        {
            SignUserOut();
            Application.Current.Shutdown();
        }

        public async static void SignUserOut()
        {
            var accounts = await PublicClientApp.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    await PublicClientApp.RemoveAsync(accounts.FirstOrDefault());
                }
                catch (MsalException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private static async Task<MsalCacheHelper> CreateCacheHelperAsync()
        {
            // Since this is a WPF application, only Windows storage is configured
            var storageProperties = new StorageCreationPropertiesBuilder(
                              System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".msalcache.bin",
                              MsalCacheHelper.UserRootDirectory)
                                .Build();

            MsalCacheHelper cacheHelper = await MsalCacheHelper.CreateAsync(
                        storageProperties,
                        new TraceSource("MSAL.CacheTrace"))
                     .ConfigureAwait(false);

            return cacheHelper;
        }
    }

}
