function showFullscreenAdv() {
    ysdk.adv.showFullscreenAdv({
        callbacks: {
            onOpen: function() {
                Module.SendMessage("YaGamesSDK", "FullscreenAdOpenCallback");
            },
            onClose: function(wasShown) {
                Module.SendMessage("YaGamesSDK", "FullscreenAdCloseCallback", Number(wasShown));
            },
            onError: function(error) {
                Module.SendMessage("YaGamesSDK", "FullscreenAdErrorCallback", String(error));
            },
            onOffline: function(){
                Module.SendMessage("YaGamesSDK", "FullscreenAdOfflineCallback");
            }
        }
    });
}

function showRewardAd(id) {
    ysdk.adv.showRewardedVideo({
        callbacks: {
            onOpen: () => {
                Module.SendMessage("YaGamesSDK", "RewardAdOpenCallback", id);                
            },
            onRewarded: () => {
                Module.SendMessage("YaGamesSDK", "RewardAdRewardedCallback", id);
            },
            onClose: () => {
                Module.SendMessage("YaGamesSDK", "RewardAdCloseCallback", id);
            }, 
            onError: (error) => {
                Module.SendMessage("YaGamesSDK", "RewardAdErrorCallback", String(error));
            }
        }
    })
}