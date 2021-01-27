import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Link as ScrollLink } from "react-scroll";
import "./Navbar.css";

function Navbar() {
  const [click, setClick] = useState(false);
  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);

  let isLogged = !!localStorage.getItem("token");

  const logOut = () => {
    localStorage.removeItem("token");
    window.location.reload();
  };

  return (
    <>
      <nav className="navbar">
        <div className="navbar-container">
          <div className="menu-icon" onClick={handleClick}>
            <i className={click ? "fas fa-times" : "fas fa-bars"}></i>
          </div>
          <ul className={click ? "nav-menu active" : "nav-menu"}>
            <li className="nav-item">
              <ScrollLink
                to="hero-container"
                offset={-90}
                smooth={true}
                spy={true}
                className="nav-links"
              >
                Home
              </ScrollLink>
            </li>
            <li className="nav-item">
              <ScrollLink
                to="experience-container"
                offset={-90}
                smooth={true}
                spy={true}
                className="nav-links"
              >
                Experience
              </ScrollLink>
            </li>
            <li className="nav-item">
              <ScrollLink
                activeClass="active"
                to="projects-container"
                spy={true}
                smooth={true}
                className="nav-links"
                onClick={closeMobileMenu}
              >
                Projects
              </ScrollLink>
            </li>
            <li className="nav-item">
              {isLogged ? (
                <span className="nav-links" onClick={logOut}>
                  Logout
                </span>
              ) : (
                <Link
                  to={{ pathname: "/login", state: { from: "/" } }}
                  className="nav-links"
                >
                  Login
                </Link>
              )}
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
}

export default Navbar;
