var script = document.createElement('script');
script.src = 'https://yandex.ru/games/sdk/v2';
document.head.appendChild(script);

function sdkInitialize(){
    try {

    

    YaGames
    .init()
    .then(ysdk => {
        window.ysdk = ysdk;
        ysdk.getStorage().then(safeStorage => { 
            try {            
                Object.defineProperty(window, 'localStorage', { get: () => safeStorage })
            }
            catch {
                console.error("Режим инкогнито");
            }
        });
        ysdk.features.LoadingAPI?.ready();
        Module.SendMessage("YaGamesSDK", "SDKInitializedCallback");
    });   
}
catch {
    setTimeout(sdkInitialize, 200);
}     
}

function setToSafeStorage(key, value) {
    try {
        localStorage.setItem(key, value);
    }
    catch {
        console.error("Данные не установлены");
    }
}

function getFromSafeStorage(key) {
    try {    
        return localStorage.getItem(key);
    }
    catch{
        console.error("Режим инкогнито");
    }
}

function getDeviceInfo(){
    return ysdk.deviceInfo.type;
}

function getEnvironment() {
    return JSON.stringify(ysdk.environment);
}