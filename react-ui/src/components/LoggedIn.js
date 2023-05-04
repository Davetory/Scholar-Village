import React, { useEffect, useState } from 'react';

import { useNavigate, useLocation } from 'react-router';
import axios from 'axios';
import './LoggedIn.css';
import Unity, { UnityContext } from "react-unity-webgl";

const unityContext = new UnityContext({
    loaderUrl: "Build/Build.loader.js",
    dataUrl: "Build/Build.data",
    frameworkUrl: "Build/Build.framework.js",
    codeUrl: "Build/Build.wasm",
  });

function LoggedIn() {
    const {state} = useLocation();
    const data = state.data;
    const [show, setShow] = useState(false)
    const navigate = useNavigate();

    function logout (e) {
        e.preventDefault();
        axios.post('/logout').then(response => {
            navigate('/');
        }).catch(response => {
            navigate('/');
        });
    }

    const [isLoaded, setIsLoaded] = useState(false);

    useEffect(function () {
        unityContext.on("loaded", function () {
            setIsLoaded(true);
        });
    }, []);
    
    
    return(
        <div>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <h1> {data.username} </h1>
                <button onClick={() => navigate('/ModifyAccount', {state: {data}})}> 
                    Modify Account
                </button>
                {/* Might have to make logout buttonclick set username, password, email back to default values of null, not sure tho */}
                <button onClick={(e) => logout(e)}>
                    Logout
                </button>
                <button onClick={()=>setShow(!show)}>
                    Access Guidebook
                </button>
                <button onClick={() => navigate('/chat', {state: {data}})}>
                    Chat
                </button>
            </nav>
            {
                show?<p className="guidebook">placeholder text for guidebook</p>:null
            }
            <div className = "game">                
                <Unity
                    unityContext={unityContext}
                    style={{
                        //visibility: isLoaded ? "visible" : "hidden",
                        height: 600,
                        width: 962,
                        border: "2px solid black",
                        background: "grey",
                     }}
                />
            </div>
        </div>
    );
}



export default LoggedIn;