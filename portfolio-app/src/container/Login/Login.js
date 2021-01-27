import React, { useState } from "react";
import { useHistory, useLocation } from "react-router";
import axios from "axios";
import "./Login.css";
import { Link } from "react-router-dom";

export default function Login() {
  let api = process.env.REACT_APP_API_URL;
  let object = { userName: "", password: "" };
  const [form, setForm] = useState(object);
  const history = useHistory();
  const location = useLocation();

  const loginHandle = (event) => {
    event.preventDefault();
    axios.post(`${api}/Identity/Login`, form).then((response) => {
      localStorage.setItem("token", response.data);

      if (location.state == undefined) {
        window.location.href = "/";
      }

      if (location.state.from !== "/register") {
        history.goBack();
      } else {
        window.location.href = "/";
      }
    });
  };

  return (
    <form onSubmit={loginHandle} className="custom-container">
      <h2 className="custom-heading">Login</h2>
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
        Don't have an account?{" "}
        <Link to="/Register" className="custom-link">
          Register!
        </Link>
      </p>
    </form>
  );
}
