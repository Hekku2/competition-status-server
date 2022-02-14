import { HubConnection, HubConnectionBuilder, IStreamSubscriber, ISubscription } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { CurrentCompetitorEnvelopeModel } from "../services/openapi";
import { useAppDispatch } from "../store";
import { setCurrentCompetitor } from "../store/competition/competitionSlice";

export interface HubConnectionProviderProps {
  baseUrl: string,
  children?: React.ReactNode
}

export const HubConnectionProvider = ({ children, baseUrl }: HubConnectionProviderProps) => {
  const dispatch = useAppDispatch()
  const [connection, setConnection] = useState<HubConnection | undefined>(undefined);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(`${baseUrl}/competition-hub`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  const competitorHandler: IStreamSubscriber<CurrentCompetitorEnvelopeModel> = {
    next: (value) => {
      dispatch(setCurrentCompetitor(value.content))
    },
    complete: () => { console.log('Finished') },
    error: () => { console.log('Error') },
    closed: false
  }

  useEffect(() => {
    let sub: ISubscription<CurrentCompetitorEnvelopeModel>;
    if (connection) {
      connection.start()
        .then(() => {
          console.log('Connected!');

          sub = connection.stream<CurrentCompetitorEnvelopeModel>('StreamCompetitors').subscribe(competitorHandler);
        })
        .catch((e: any) => console.log('Connection failed: ', e));
    }
    return () => {
      sub?.dispose()
    }
  }, [connection, competitorHandler]);

  return (
    <>
      {children}
    </>
  );
}
