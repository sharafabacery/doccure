(function ($) {
    "use strict";
    const signalingServer = new signalR.HubConnectionBuilder()
        .withUrl("/mediaHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    const localVideo = document.getElementById('video-sender');
    const remoteVideo = document.getElementById('video-reciver');
    var elapsedTime = 0; // Start time in seconds (initially 0)
    var timerDisplay = document.getElementById("timerDisplay");

    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    
    let localStream;
    let type = params.type;

    let meetingId = params.meetingId
    let peerConnection;
    function startTimer() {
        setInterval(function () {
            elapsedTime++;
            timerDisplay.innerHTML = formatTime(elapsedTime);
        }, 1000); // Update every 1 second
    }

    // Format the time as HH:MM:SS
    function formatTime(seconds) {
        var hrs = Math.floor(seconds / 3600);
        var mins = Math.floor((seconds % 3600) / 60);
        var secs = seconds % 60;

        return `${pad(hrs)}:${pad(mins)}:${pad(secs)}`;
    }

    // Pad single digits with leading zeros
    function pad(num) {
        return num < 10 ? "0" + num : num;
    }
    signalingServer.on("AddConection", async() => {
        try {
            await signalingServer.invoke('AddConnection', meetingId);
            console.log("SignalR Connected.");
        } catch(err) {
            console.log(err);
            //setTimeout(start, 5000);
        }
        })
    async function start() {
        try {
            await signalingServer.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            //setTimeout(start, 5000);
        }
    };
    async function startCall() {
        // Get local media stream
        localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        localVideo.srcObject = localStream;

        // Initialize PeerConnection
        peerConnection = new RTCPeerConnection();

        // Add local stream to PeerConnection
        localStream.getTracks().forEach(track => peerConnection.addTrack(track, localStream));

        // Handle remote stream
        peerConnection.ontrack = (event) => {
            remoteVideo.srcObject = event.streams[0];
        };

        // Signal handling
        signalingServer.on("receiveOffer", async (offer) => {
            console.log(offer)
            await peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse( offer)));
            const answer = await peerConnection.createAnswer();
            await peerConnection.setLocalDescription(answer);
            
            await signalingServer.invoke("sendAnswer", meetingId, JSON.stringify(answer));
            
        });

        signalingServer.on("receiveAnswer", async (answer) => {
            await peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse(answer)));
        });

        signalingServer.on("receiveIceCandidate", async (candidate) => {
            console.log(candidate)
            await peerConnection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)));
        });

        // ICE Candidate handling
        peerConnection.onicecandidate = async (event) => {
            if (event.candidate) {
                await signalingServer.invoke("sendIceCandidate", meetingId, JSON.stringify(event.candidate));
            }
        };

        if (type == 'sender') {
const offer = await peerConnection.createOffer();
        await peerConnection.setLocalDescription(offer);
        await signalingServer.invoke("sendOffer", meetingId, JSON.stringify( offer));
        }
        // Create offer and send it to the remote peer
        
    }
    start()
    startCall()
    window.onload = startTimer;
    
    
   
  
})(jQuery);