using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace OpenShockSaber
{
    public class OpenShockManager
    {
        private DateTime _lastShockTime = DateTime.MinValue;
        private static readonly HttpClient _client = new HttpClient();
        private static readonly System.Random _random = new System.Random();

        public async void RequestShock()
        {
            if ((DateTime.Now - _lastShockTime).TotalSeconds < OpenShockSaberConfig.Instance.CooldownSeconds)
                return;

            _lastShockTime = DateTime.Now;

            int intensity = _random.Next(
                OpenShockSaberConfig.Instance.MinIntensity, 
                OpenShockSaberConfig.Instance.MaxIntensity + 1
            );

            var payload = new
            {
                shocks = new[]
                {
                    new {
                        id = OpenShockSaberConfig.Instance.DeviceID,
                        type = "shock",
                        intensity = intensity,
                        duration = OpenShockSaberConfig.Instance.DurationMs
                    }
                }
            };

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, OpenShockSaberConfig.Instance.APIEndpoint);
                request.Headers.Add("OpenShockToken", OpenShockSaberConfig.Instance.APIKey);
                request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                await _client.SendAsync(request);
                Plugin.Log.Info($"OpenShockSaber: Sent {intensity}% shock.");
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"OpenShockSaber API Error: {ex.Message}");
            }
        }
    }
}