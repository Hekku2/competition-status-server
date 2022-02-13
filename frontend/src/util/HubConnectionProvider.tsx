import { HubConnection, HubConnectionBuilder, IStreamSubscriber } from "@microsoft/signalr";
import { Card, CardContent, CardHeader, List } from "@mui/material";
import { createNextState } from "@reduxjs/toolkit";
import { useEffect, useState } from "react";
import { CurrentCompetitorEnvelopeModel } from "../services/openapi";

export interface HubConnectionProviderProps {
  children?: React.ReactNode
}

export const HubConnectionProvider = ({ children }: HubConnectionProviderProps) => {
  const [connection, setConnection] = useState<HubConnection | undefined>(undefined);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/competition-hub')
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  const x: IStreamSubscriber<CurrentCompetitorEnvelopeModel> = {
    next: (value) => { console.log(value) },
    complete: () => { },
    error: () => { },
    closed: false
  }

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(() => {
          console.log('Connected!');

          connection.stream<CurrentCompetitorEnvelopeModel>('StreamCompetitors').subscribe(x);
        })
        .catch((e: any) => console.log('Connection failed: ', e));
    }
  }, [connection]);

  return (
    <>
      {children}
    </>
  );
}