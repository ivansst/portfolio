import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Portfolio from "../Portfolio/Portfolio";
import Login from "../Login/Login";
import Register from "../Register/Register";
import Project from "../Project/Project";
import CustomError from "../Error/Error";
import "./App.css";

function App() {
  return (
    <>
      <Router>
        <Switch>
          <Route exact path="/" component={Portfolio} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/register" component={Register} />
          <Route exact path="/project/:id" component={Project} />
          <Route exact path="/error" component={CustomError} />
        </Switch>
      </Router>
    </>
  );
}

export default App;
