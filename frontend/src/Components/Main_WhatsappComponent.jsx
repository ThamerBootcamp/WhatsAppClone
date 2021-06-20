import React, { useState } from 'react'
import { Container, Row, Col } from 'react-bootstrap';
import Chat from './ChatComponent'
import Search from './SearchComponent'

import { joinRoom } from '../Chat'

export default function Main_WhatsappComponeat({ token, id }) {
    const [chatList, setChatList] = useState([])
    const [selectedChat, setSelectedChat] = useState(null)
    const [room, setRoom] = useState(null)

    function selectUser(user){
        setSelectedChat(user)
        setRoom([user.id, id].sort().join(""))
        joinRoom([user.id, id].sort().join(""))
    }
    
    function addUser(user){
        console.log(user)
        var isHere = false;
        chatList.forEach(u => {
            if(u && u.id == user.id){
                isHere = true
            }
            selectUser(u)
        })

        if(!isHere){
            setChatList([...chatList, user])
            selectUser(user)
        }
    }
    return (
        <div style={{paddingTop:"50px"}} className="MainChat">
            <Container>
                <Row>
                    { selectedChat && <Col sm={8} ><Chat room={room} token={token} selectedChat={selectedChat} /></Col> }
                    <Col sm={4}><Search addUser={addUser} token={token} chatList={chatList} setSelectedChat={selectUser} /></Col>
                </Row>
            </Container>
        </div>
    )
}
