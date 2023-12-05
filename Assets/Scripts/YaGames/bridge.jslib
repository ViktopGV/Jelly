mergeInto(LibraryManager.library, {

    SDKInitialize: function () {
        sdkInitialize();
    },

    LoadingApiReady: function(){
        loadingApiReady();  
    },

    ShowFullscreenAdv: function() {
        showFullscreenAdv();
    },

    ShowRewardAd: function(id) {
        showRewardAd(id);
    },

    PlayerInitialize: function(scopes = false) {
        scopes = Boolean(scopes);
        initializePlayer(scopes);
    },

    IsPlayerAuth: function() {
        return isPlayerAuth();
    },

    AuthorizePlayer: function() {
        authorizePlayer();
    },

    SetPlayerData: function(dataJson) {
        dataJson = UTF8ToString(dataJson);
        data = JSON.parse(dataJson);
        setPlayerData(data);
    },

    GetPlayerData: function() {
        getPlayerData();
    },

    GetPlayerUniqueID: function(){
        var str = getPlayerUniqueID();
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    GetPlayerName: function() {
        var str = getPlayerName();
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    GetPlayerPhoto: function(size) {
        switch(size){
            case 0:
                size = 'small';
                break;
            case 1:
                size = 'medium';
                break;
            case 2:
                size = 'large';
                break;
        }
        var str = getPlayerPhoto(size);
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    GetEnvironmentData: function() {
        var str = getEnvironment();
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    GetDeviceInformation: function(){
        var str = getDeviceInfo();
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    SetData: function(key, value) {
        key = UTF8ToString(key);
        value = UTF8ToString(value);
        setToSafeStorage(key, value);
    },

    GetData: function(key) {
        key = UTF8ToString(key);
        var str = getFromSafeStorage(key);
        if(str != null) { // was !==
            var bufferSize = lengthBytesUTF8(str) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(str, buffer, bufferSize);
            return buffer;
        }
        else {
            str = "NullValue"
            var bufferSize = lengthBytesUTF8(str) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(str, buffer, bufferSize);
            return buffer;
        }
    },

    LeaderboardInitialize: function() {
        initializeLeaderboard();
    },

    GetLeaderboardDescription: function(leaderboard) {
        getLeaderboardDescription(UTF8ToString(leaderboard));
    },

    SetLeaderboardScore: function(leaderboard, score, extraData) {
        leaderboard = UTF8ToString(leaderboard);
        extraData = UTF8ToString(extraData);
        extraData = extraData == "NullExtraData" ? null : extraData;
        setLeaderboardScore(leaderboard, score, extraData);
    },

    GetLeaderboardPlayerEntry: function(leaderboard) {
        leaderboard = UTF8ToString(leaderboard);
        getLeaderboardPlayerEntry(leaderboard);
    },

    GetLeaderboardEntries: function(leaderboard, quantityTop, quantityAround, includeUser) {
        leaderboard = UTF8ToString(leaderboard);
        includeUser = Boolean(includeUser);
        getLeaderboardEntries(leaderboard, quantityTop, includeUser, quantityAround);
    }

  
  });