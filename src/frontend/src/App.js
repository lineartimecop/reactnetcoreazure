/* ****************************************************************************
 * File name: App.js
 *
 * Author: Tamás Kiss
 * Created: Oct/15/2024
 *
 * Last Editor: Tamás Kiss
 * Last Modified: Nov/11/2024
 *
 * Copyright (C) Tamás Kiss, 2024.
 * ************************************************************************* */

import { useEffect, useState } from "react";
import "./App.css";

const backEndPortDev = 5000;

function App() {
    const [data, setData] = useState([]);
    const [colNames, setColNames] = useState([]);

    useEffect(() => {
        // Get data from backend when page loads
        getData();
    }, []);

    var protocol = window.location.protocol;
    var server = window.location.hostname;

    var baseUrl = protocol + "//" + server;
    if (server == "localhost") {
        baseUrl += ":" + backEndPortDev;
    }
    else {
        baseUrl += "/WebApp.backend";
    }

    var testDataUrl = baseUrl + "/data/Test";
    var dbDataUrl = baseUrl + "/data/WebApp";

    const getData = async () => {
        fetch(dbDataUrl)
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (data.status == 0) {
                    setData(data.data);
                    return Object.keys(data.data[0]);
                }
                else {
                    setData([]);
                    return Object.keys([]);
                }
            })
            .then(firstRecord => {
                setColNames(firstRecord);
            });
    }

    return (
        <>
            <table>
                <thead>
                    {
                        Object.values(colNames).map(value => {
                            return <th>{value}</th>;
                        })
                    }
                </thead>
                <tbody>{data.map(obj => {
                    return <tr>{Object.values(obj).map(value => {
                        return <td>{value}</td>;
                    })}</tr>;
                })}
                </tbody>
            </table>
        </>
    );
}

export default App;
