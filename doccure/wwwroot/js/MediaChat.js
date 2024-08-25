(function ($) {
    "use strict";
    const connectionhub = new signalR.HubConnectionBuilder()
        .withUrl("/mediaHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    console.log(connectionhub)
    
    const webrtc = new RTCPeerConnection()
    webrtc.onicecandidate = e => { iceCandiate = JSON.stringify(webrtc.localDescription); console.log(iceCandiate); }

    webrtc.addEventListener('track', event => {
        if (remoteVideo.srcObject !== event.streams[0]) {
            console.log(event)
            remoteStream = event.streams[0];
            remoteVideo.srcObject = remoteStream;
            console.log('tracking')
        }
        console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
    });
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    const localVideo = document.getElementById('video-sender');
    const remoteVideo = document.getElementById('video-reciver');
    let localStream;
    let remoteStream;
    let meetingId = params.meetingId
    let type = params.type
    let iceCandiate = {}
    const mediaStreamConstraints = {
        video: true,
        audio: true
    };
    EnableSending()
    start()

    async function start() {
        try {
            await connectionhub.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            //setTimeout(start, 5000);
        }
    };
    async function EnableSending() {
        try {
            const mediaStream = await navigator.mediaDevices.getUserMedia(mediaStreamConstraints);
            localVideo.srcObject = mediaStream;
            localStream = mediaStream;
            localStream.getTracks().forEach(track => {
                webrtc.addTrack(track, localStream);
            });
        } catch (err) {
            console.log(err);
            //setTimeout(start, 5000);
        }
    };
    connectionhub.on('Connected', async () => {

        try {
            await connectionhub.invoke("AddConnection", meetingId);
            
        } catch (e) {
            console.log(e)
        }
    })
    async function creationOffer() {
        await webrtc.createOffer()
            .then(e => webrtc.setLocalDescription(e))
            .then(e => {
                console.log('local decription set for sender');
                //iceCandiate = JSON.stringify(webrtc.localDescription);
            })

    }
    async function answerCreation() {
        await webrtc.createAnswer()
            .then(e => webrtc.setLocalDescription(e))
            .then(e => {
                console.log('local decription set for sender');
                //iceCandiate = JSON.stringify(webrtc.localDescription);
            })
    }
    connectionhub.on("Info", async(msg) => {
       
        if (type == "sender") {
            await creationOffer()            
            try {
                
                await connectionhub.invoke("Signaling", meetingId, iceCandiate);
                console.log('Invoking Signaling of sender')
            } catch (e) {
                console.log(e)
            }
        }
    })
    connectionhub.on('SDP', async (sdp) => {
        console.log(sdp)
        webrtc.setRemoteDescription(JSON.parse( sdp)).then(e => console.log('Remote decription set for ' + type))
        if (type == "reciver") {
            await answerCreation()
            try {
                await connectionhub.invoke("Signaling", meetingId, iceCandiate);
                console.log('Invoking Signaling of reciver')
            } catch (e) {
                console.log(e)
            }
        } 
    })
  
})(jQuery);