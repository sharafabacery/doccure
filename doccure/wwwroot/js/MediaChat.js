(function ($) {
    "use strict";
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/mediahub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    const icecongigration = {
        iceServers: [{
'urls': "stun1.l.google.com:19302"
            }
            
            ]
        }
    const webrtc = new RTCPeerConnection(icecongigration)
    
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let meetingId = params.meetingId
    let type = params.type
    let iceCandiate = ''
    webrtc.onicecandidate(e => iceCandiate = JSON.stringify(webrtc.localDescription))
    async function addConnection(meeting) {
        try {
            await connection.invoke("AddConnection", meeting);
        } catch (e) {
            console.log(e)
        }
    }
    addConnection(meetingId);
    connection.on("Info", (msg) => {
        console.log(msg)
        if (type == "sender") {
            webrtc.createOffer().then(e => webrtc.localDescription(e)).then(e => console.log('local decription set for sender'))
            try {
                await connection.invoke("Signaling", meetingId, iceCandiate);
                console.log('Invoking Signaling of sender')
            } catch (e) {
                console.log(e)
            }
        }
    })
    connection.on('SDP', (sdp) => {
        webrtc.setRemoteDescription(sdp).then(e => console.log('local decription set for ' + type))
        if (type == "reciver") {
            webrtc.createAnswer().then(a => webrtc.localDescription(a)).then(e => console.log('local decription set for reciver'))
            try {
                await connection.invoke("Signaling", meetingId, iceCandiate);
                console.log('Invoking Signaling of reciver')
            } catch (e) {
                console.log(e)
            }
        } 
    })

})(jQuery);