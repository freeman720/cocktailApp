import * as React from "react";
import { NavMenu } from "./NavMenu";

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    render() {
        return <div className="container-fluid">
            <div className="row">
                <div className="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    <NavMenu />
                </div>
                <div className="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                    { this.props.children }
                </div>
            </div>
        </div>;
    }
}
