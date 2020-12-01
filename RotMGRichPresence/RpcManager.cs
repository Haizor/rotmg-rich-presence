using System;

namespace RotMGRichPresence {
    public class RpcManager {
        public static Discord.Discord discord;
        public static Discord.Activity act;
        public static void Init() {
            discord = new Discord.Discord(625092334283128892, (ulong)Discord.CreateFlags.NoRequireDiscord);
            act = new Discord.Activity();
            
            act.Type = Discord.ActivityType.Playing;
            act.Timestamps.Start = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            act.State = "Loading...";
            act.Assets.LargeImage = "icon";

            discord.GetActivityManager().UpdateActivity(act, OnActivityUpdate);
            discord.RunCallbacks();
        }

        public static void UpdateStatus() {
            if (discord != null) {
                if (BepInExLoader.configShowServer.Value) {
                    act.Details = DataGrabber.server;
                } else {
                    act.Details = "Hidden";
                }

                act.State = DataGrabber.location;

                if (DataGrabber.className != null) {
                    act.Assets.SmallImage = DataGrabber.className.ToLower();
                    act.Assets.SmallText = DataGrabber.className;
                }

                discord.GetActivityManager().UpdateActivity(act, OnActivityUpdate);
                discord.RunCallbacks();
            }
        }

        public static void Close() {
            discord.GetActivityManager().ClearActivity((p) => { });
            discord.Dispose();
        }

        public static void OnActivityUpdate(Discord.Result result) {

        }
    }
}
