import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from "./Card";
import "./CardsSection.css";

function CardsSection() {
  let api = process.env.REACT_APP_API_URL;
  console.log(api);
  const [cards, setCards] = useState([]);

  const fetchProjects = () => {
    let token = `bearer ${localStorage.getItem("token")}`;
    axios.defaults.headers.common.Authorization = token;

    axios
      .get(`${api}/projects/getall`, { token })
      .then((response) => {
        setCards(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    fetchProjects();
  }, []);

  return (
    <div className="projects-container">
      <h1 className="card-text">Projects</h1>
      <section className="card-list">
        {cards.map((card) => {
          return (
            <Card
              key={card.id}
              id={card.id}
              Title={card.title}
              ShortDescription={card.description}
            />
          );
        })}
      </section>
    </div>
  );
}

export default CardsSection;
