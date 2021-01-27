import React from "react";
import "./Experience.css";

function Experience() {
  return (
    <div className="experience-container">
      <div className="work">
        <div className="title">Work Experience</div>
        <div className="list">
          <ul>
            <li>
              .NET Developer at Beluga IT
              <ul className="specifications">
                <li>August 2019 - ongoing</li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
      <hr className="line" />
      <div className="technologies">
        <div className="title">Technologies</div>
        <div className="list">
          <ul className="list-technologies">
            <li>.NET Core</li>
            <li>MySQL</li>
            <li>Git</li>
            <li>HTML&CSS</li>
            <li>JavaScript</li>
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Experience;
