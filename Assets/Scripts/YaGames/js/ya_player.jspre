function initializePlayer(scopes = false) {
    
    if(typeof window.player === 'undefined') {
        ysdk.getPlayer({scopes : scopes}).then(_player => {
            player = _player;
            Module.SendMessage("YaGamesSDK", "PlayerInitializedCallback");
        }).catch(err => {
            console.log(err);
        });
    }
    else {
        ysdk.getPlayer({scopes : player.config.scopes}).then(_player => {
            player = _player;
        }).catch(err => {
            console.log(err);
        });
    }
}

function isPlayerAuth() {
    if(typeof window.player === 'undefined') return false;               
    return !(player.getMode() === 'lite')            
}

function authorizePlayer() {
    if(isPlayerAuth() == false) {
    ysdk.auth.openAuthDialog().then(() => {
            Module.SendMessage("YaGamesSDK", "PlayerHasLoggedInCallback");
            initializePlayer();
        }).catch(() => {
            Module.SendMessage("YaGamesSDK", "PlayerRefusedAuthorizationCallback");
        });
    }
}

function setPlayerData(data) {
    player.setData({ data });
}

function getPlayerData() {
    player.getData().then(result => {     
        data = JSON.stringify(result['data']);

        if(typeof data === 'undefined'){
            data = "NullValue"
        }
        Module.SendMessage("YaGamesSDK", "GotPlayerDataCallback", data)
    });
             

}

function getPlayerUniqueID(){
    id = player.getUniqueID();
    return id
}

function getPlayerName(){
    return player.getName();
}

function getPlayerPhoto(size){
    console.log(player.getPhoto(size));
    return player.getPhoto(size);
}
