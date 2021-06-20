import React, { useState } from 'react'
import { InputGroup, FormControl, Button } from 'react-bootstrap';

import { SendMessage, onMessage } from '../Chat'


export default function Chat({ token, room, selectedChat }) {
    const [meesage, setMessage] = useState("")
    const [meesageList, setMeesageList] = useState([])

    function _SendMessage(){
        SendMessage(token, room, selectedChat.id, meesage)
        setMessage("");
    }

    onMessage(function (message, receiverId) {
        if(receiverId != selectedChat.id){
            setMeesageList([ ...meesageList,  <li style={{ width: "100px" }} className="sender text-start">{message}</li>])
        } else {
            setMeesageList([ ...meesageList,  <li style={{ width: "100px" }} className="receive-chat text-end">{message}</li>])
        }
    })

    return (
        <div className="chat">
            <div style={{ height: "550px", position: "relative" }} >
                <ul style={{ position: "absolute", bottom: " 0px", listStyleType: "none", width:"100%" }}>
                    {
                        meesageList
                    }
                </ul>
            </div>
            <InputGroup className="mb-3">
                <FormControl value={meesage} onChange={(e) => setMessage(e.target.value)} style={{backgroundColor:"#1f2428" ,color:"white"}} placeholder="Type a message" aria-label="Recipient's username" aria-describedby="basic-addon2" />
                <Button variant="outline-secondary" id="button-addon2"  className="buttonChat" onClick={_SendMessage}>
                    Send
                </Button>
            </InputGroup>
        </div>
    )
}
