import React, { useState } from "react";
import { Link, Redirect } from "react-router-dom";
import axios from "axios";
import "./Register.css";

export default function Register() {
  let object = { userName: "", email: "", password: "" };
  const [form, setForm] = useState(object);
  const [isSuccessful, setIsSuccessful] = useState(false);

  const registerHandle = (event) => {
    event.preventDefault();
    axios
      .post("https://localhost:44381/Identity/Register", form)
      .then((response) => {
        setIsSuccessful(true);
      });
  };

  return (
    <form onSubmit={registerHandle} className="custom-container">
      {isSuccessful ? (
        <Redirect to={{ pathname: "/login", state: { from: "/register" } }} />
      ) : null}
      <h2 className="custom-heading">Register</h2>
      <div className="form-group">
        <label className="custom-label">Username</label>
        <input
          className="custom-input"
          type="text"
          placeholder="Username"
          onChange={(event) => {
            let value = event.target.value;
            setForm((previousState) => {
              return { ...previousState, userName: value };
            });
          }}
        />
      </div>

      <div className="form-group">
        <label className="custom-label">Email</label>
        <input
          className="custom-input"
          type="text"
          placeholder="Email"
          onChange={(event) => {
            let value = event.target.value;
            setForm((previousState) => {
              return { ...previousState, email: value };
            });
          }}
        />
      </div>

      <div className="form-group">
        <label className="custom-label">Password</label>
        <input
          className="custom-input"
          type="password"
          placeholder="Password"
          onChange={(event) => {
            let value = event.target.value;
            setForm((previousState) => {
              return { ...previousState, password: value };
            });
          }}
        />
      </div>

      <button type="submit" className="custom-btn">
        Submit
      </button>
      <p className="custom-text">
        Already registered?{" "}
        <Link to="/Login" className="custom-link">
          Sign In!
        </Link>
      </p>
    </form>
  );
}
