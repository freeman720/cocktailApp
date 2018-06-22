import * as React from "react";
import { Link, NavLink } from "react-router-dom";

export class NavMenu extends React.Component<{}, {}> {
    render() {
        return <div className="main-nav" >
                <div className="navbar navbar-inverse">
                <div className="navbar-header">
                    <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                    <Link className="navbar-brand" to={ "/" }>CocktailApp</Link>
                </div>
                <div className="clearfix"></div>
                <div className="navbar-collapse collapse">
                    <ul className="nav navbar-nav">
                        <li>
                            <NavLink to={ "/" } exact activeClassName="active">
                                <span className="glyphicon glyphicon-glass"></span> Cocktails
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ "/admin" } activeClassName="active">
                                <span className="glyphicon glyphicon-user"></span> Admin
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/fetchdata"} activeClassName="active">
                                <span className="glyphicon glyphicon-user"></span> Fetch Data
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/counter"} activeClassName="active">
                                <span className="glyphicon glyphicon-headphones"></span> Counter
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}