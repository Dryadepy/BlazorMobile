﻿using BlazorMobile.Common.Services;
using BlazorMobile.Components;
using BlazorMobile.Interop;
using System;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace Microsoft.AspNetCore.Builder
{
    public static class BlazorHostingApplicationBuilderExtensions
    {
        /// <summary>
        /// Call of this method will notify to BlazorMobile that the runtime is actually running under ElectronNET (server-side)
        /// NOTE: Usage of blazor.server.js in your starting page is mandatory.
        /// If using BlazorMobile.Build on your Blazor base project, you should register 'server_index.html' at this app start.
        /// </summary>
        public static IApplicationBuilder UseBlazorMobileWithElectronNET<TFormsApplication>(this IApplicationBuilder app) where TFormsApplication : BlazorApplication
        {
            ContextHelper.SetElectronNETUsage(true);

            //Bridging Native receive method in memory on ElectronNET implementation
            ContextHelper.SetNativeReceive(ContextBridge.Receive);

            app.UseStaticFiles();

            Forms.Init();

            Forms.LoadApplication((TFormsApplication)Activator.CreateInstance(typeof(TFormsApplication)));

            return app;
        }
    }
}