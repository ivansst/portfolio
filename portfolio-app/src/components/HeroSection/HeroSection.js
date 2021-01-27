import React from "react";
import "./HeroSection.css";

function HeroSection() {
  return (
    <div className="hero-container">
      <div className="hero-information">
        <h1>Hello, I'm Ivan.</h1>
        <h2>I'm a full-stack web developer.</h2>
      </div>
      <div className="hero-image">
        <img src="/IMG_4661.PNG" alt="" />
      </div>
    </div>
  );
}

export default HeroSection;
