function initializeLeaderboard() {
    ysdk.getLeaderboards().then(_lb => { 
        lb = _lb;
        Module.SendMessage("YaGamesSDK", "LeaderboardInitializedCallback");
    }
    );
}

function getLeaderboardDescription(leaderboardName){
    lb.getLeaderboardDescription(leaderboardName)
    .then(res => {
        Module.SendMessage("YaGamesSDK", "GetLeaderboardDescriptionCallback", JSON.stringify(res));
    });
}

function setLeaderboardScore(leaderboard, score, extraData = null) {
    if(isPlayerAuth()){
        if(extraData == null)
            lb.setLeaderboardScore(leaderboard, score);
        else
            lb.setLeaderboardScore(leaderboard, score, extraData);
    }
}


function getLeaderboardPlayerEntry(leaderboard) {
    lb.getLeaderboardPlayerEntry(leaderboard)
    .then(res => Module.SendMessage("YaGamesSDK", "GetLeaderboardPlayerEntryCallback", JSON.stringify(res)))
    .catch(err => {
        if (err.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
            console.log("Игрока нет в таблице");
        }
    });
}

function getLeaderboardEntries(leaderboard, quantityTop = 5, includeUser = true, quantityAround = 5) {
    lb.getLeaderboardEntries(leaderboard, { quantityTop: quantityTop, includeUser: includeUser, quantityAround: quantityAround })
    .then(res => {
        for(var i = 0; i < res.entries.length; ++i){
            res.entries[i].player.avatarSrc = res.entries[i].player.getAvatarSrc("small");
            res.entries[i].player.avatarSrcSet = res.entries[i].player.getAvatarSrcSet("small");
        }
        Module.SendMessage("YaGamesSDK", "GetLeaderboardEntriesCallback", JSON.stringify(res))
    });
}