import React from "react";
import { Link } from "react-router-dom";
import "./Card.css";

function Card(props) {
  return (
    <Link className="text-card" to={`/project/${props.id}`}>
      <article className="card">
        <header className="card-header">
          <h2>{props.Title}</h2>
          <p>{props.ShortDescription}</p>
        </header>
      </article>
    </Link>
  );
}

export default Card;
