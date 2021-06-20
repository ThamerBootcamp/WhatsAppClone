import React from 'react'
import { Form, Button, Col, Row, Alert } from "react-bootstrap";

import { useState } from 'react';
import { useHistory } from "react-router-dom";

import axios from 'axios';

export default function SignUp() {

    const [fullNameState , setFullNameState] = useState("");
    const [usernameState , setUsernameState] = useState("");
    const [passwordState , setPasswordState] = useState("");
    const [imageState , setImageState] = useState("");
    const [confirmPasswordState , setConfirmPasswordState] = useState("");
    const [errorState , setErrorState] = useState(null);

    const history = useHistory();

    function register(){
        if(fullNameState.trim() == ""){
            setErrorState("Full name can not be empty");
            return;
          }
        if(usernameState.trim() == ""){
            setErrorState("User name can not be empty");
            return;
          }
    
          if(passwordState.trim() == ""){
            setErrorState("password can not be empty");
            return;
          }

          if(confirmPasswordState !== passwordState){
            setErrorState("password does not match confirm password");
            return;
          }

          if(imageState.trim() == ""){
            setErrorState("image can not be empty");
            return;
          }
          
          axios.post("https://localhost:44388/api/auth/register", { 
              Username: usernameState, 
              Displayname: fullNameState,
              Img: imageState,
              Password: passwordState
            }).then(res => {
            history.push("/login");
          }).catch(err => {
            setErrorState(err.message)
          })
    }

    return (
        <div>
            <Form className="mt-5" className='container' style={{ color : "white" ,marginBottom:"20px",padding:"10px"}}>
                <Row className="justify-content-center mt-5 ">
                    <Col md={8}>
                        <Form.Row>
                            <Col md={12}>
                            { errorState && <p style={{ color: "red" }} >{errorState}</p> }
                                <Form.Label>Full Name</Form.Label>
                                <Form.Control
                                    placeholder="Full name"
                                    name="name"
                                    style={{textAlign:"center",width:"400px",marginLeft:"160px"}}
                                    required 
                                    value={fullNameState}
                                    onChange={(e) =>  setFullNameState(e.target.value)}
                                    />
                            </Col>
                        </Form.Row>
                        <Form.Row >
                            <Col md={12}>
                                <Form.Group >
                                    <Form.Label >User Name</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="User Name"
                                        name="Username"
                                        style={{textAlign:"center",width:"400px",marginLeft:"160px"}}
                                        required
                                        value={usernameState}
                                        onChange={(e) =>  setUsernameState(e.target.value)}
                                        />
                                </Form.Group>
                            </Col>
                        </Form.Row>
                        <Form.Row>
                            <Col md={12}>
                                <Form.Group controlId="formGridPassword">
                                    <Form.Label>Image</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="Image Url"
                                        name="Image"
                                        style={{textAlign:"center",width:"400px",marginLeft:"160px"}}
                                        required
                                        value={imageState}
                                        onChange={(e) =>  setImageState(e.target.value)}
                                        />
                                </Form.Group>
                            </Col>
                        </Form.Row>
                        <Form.Row>
                            <Col md={12}>
                                <Form.Group controlId="formGridPassword">
                                    <Form.Label>Password</Form.Label>
                                    <Form.Control
                                        type="password"
                                        placeholder="Password"
                                        name="Password"
                                        style={{textAlign:"center",width:"400px",marginLeft:"160px"}}
                                        required
                                        value={passwordState}
                                        onChange={(e) =>  setPasswordState(e.target.value)}
                                        />
                                </Form.Group>
                            </Col>
                        </Form.Row>
                       
                        <Form.Row >
                           
                            <Col md={12}>
                                
                                <Form.Group controlId="formGridPassword"  >
                                    <Form.Label>Confirm Password</Form.Label>
                                    <Form.Control
                                        type="password"
                                        placeholder="Confirm Password"
                                        name="Confirm Password"
                                        style={{textAlign:"center",width:"400px",marginLeft:"160px"}}
                                        required
                                        value={confirmPasswordState}
                                        onChange={(e) =>  setConfirmPasswordState(e.target.value)}
                                        />
                                </Form.Group>
                               
                            </Col>
                        </Form.Row>
                        


                        <p>You alredy have an account ?  <a href="/login"> log in</a></p>

                        <Button
                            variant="outline-secondary"
                            onClick={register}
                        >
                            Submit
                        </Button>
                    </Col>
                </Row>
            </Form>
        </div>
    )
}
