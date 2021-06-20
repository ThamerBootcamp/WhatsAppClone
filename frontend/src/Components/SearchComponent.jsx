import React,{useState, useEffect} from 'react'
import { InputGroup, FormControl, Button } from 'react-bootstrap';
import CardSearchHelp from './CardSearchHelpComponent'


import Users from './UsersComponent';
import axios from 'axios';


export default function Search({ token, addUser, chatList, setSelectedChat }) {
    const [Search,setSearch]=useState('')

    const [searchHelp,setsearchHelp]=useState([])

    useEffect(async() => {
        var res = await axios.get("https://localhost:44388/api/UserModels", { headers: {"authorization": token} });
        setsearchHelp(res.data)
        console.log(res.data)
    }, [])
    const onChangeInput = (event) => {
        setSearch(event.target.value)
      };

      const allsearchHelp = searchHelp.filter((val)=>{
        if(Search == ""){
            return null
        }
        else if(val.username.toLowerCase().includes(Search.toLowerCase())){
            return val.username
        }
    }).map((searchHelp, i) => {
        return <CardSearchHelp key={i} searchHelp= {searchHelp.username} addUser={addUser} user={searchHelp}  />
    });

 

    return (
        <div style={{position:"relative",backgroundColor:"#131c21" }} className="search">
            <InputGroup >
                <FormControl style={{backgroundColor:"#1f2428" ,color:"white"}}  placeholder="Search" aria-label="Recipient's username" aria-describedby="basic-addon2" onChange={(e) => onChangeInput(e)}  />
            </InputGroup>
                <div style={{position:"absolute",backgroundColor:"#1f2428",width:"100%",zIndex:"2"}}>
                {allsearchHelp}
                </div>
            <Users chatList={chatList} setSelectedChat={setSelectedChat} />
        
        </div>
    )
}
