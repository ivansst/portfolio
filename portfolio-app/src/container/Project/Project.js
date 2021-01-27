import { React, useState, useEffect } from "react";
import "./Project.css";
import Comments from "../../components/Comments/Comments";
import { Redirect } from "react-router-dom";
import { useParams } from "react-router";
import axios from "axios";
import Navbar from "../../components/Navbar/Navbar";

export default function Project() {
  let api = process.env.REACT_APP_API_URL;
  let { id } = useParams();

  let token = `bearer ${localStorage.getItem("token")}`;
  axios.defaults.headers.common.Authorization = token;

  const [project, setProject] = useState([]);
  const [error, setError] = useState(false);

  const getProject = () => {
    axios
      .get(`${api}/Projects/${id}`, { token })
      .then((response) => {
        setProject(response.data);
      })
      .catch(() => {
        setError(true);
      });
  };

  const isLogged = !!localStorage.getItem("token");

  useEffect(() => {
    getProject();
  }, []);

  if (error) {
    return <Redirect to="/error" />;
  }

  if (!isLogged) {
    return <Redirect to={{ pathname: `/login`, state: { projectId: id } }} />;
  }
  return (
    <>
      <Navbar />
      <div className="row-single">
        <div className="row">
          <img src="/logo192.png" alt="" />
          <h2 className="title">{project.title}</h2>
          <p className="description">{project.description}</p>
        </div>
        <div className="row"></div>
        <Comments projectId={id} />
      </div>
    </>
  );
}
