import { HubConnection, HubConnectionBuilder, IStreamSubscriber, ISubscription } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { CurrentCompetitorEnvelopeModel, PerformanceResultsEnvelopeModel } from "../services/openapi";
import { useAppDispatch } from "../store";
import { setCurrentCompetitor, setLatestResults } from "../store/competition/competitionSlice";

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
    return () => {
      newConnection.stop()
    }
  }, [baseUrl]);

  useEffect(() => {
    const competitorHandler: IStreamSubscriber<CurrentCompetitorEnvelopeModel> = {
      next: (value) => {
        dispatch(setCurrentCompetitor(value.content))
      },
      complete: () => { console.log('Finished') },
      error: () => { console.log('Error') },
      closed: false
    }
    const performanceResultsHandler: IStreamSubscriber<PerformanceResultsEnvelopeModel> = {
      next: (value: PerformanceResultsEnvelopeModel) => {
        dispatch(setLatestResults(value.content))
      },
      complete: () => { console.log('Finished') },
      error: () => { console.log('Error') },
      closed: false
    }

    let currentCompetitorSub: ISubscription<CurrentCompetitorEnvelopeModel>;
    let performanceResultSub: ISubscription<PerformanceResultsEnvelopeModel>;
    if (connection) {
      connection.start()
        .then(() => {
          console.log('Connected!');

          currentCompetitorSub = connection.stream<CurrentCompetitorEnvelopeModel>('StreamCompetitors').subscribe(competitorHandler);
          performanceResultSub = connection.stream<PerformanceResultsEnvelopeModel>('StreamPerformanceResults').subscribe(performanceResultsHandler);
        })
        .catch((e: any) => console.log('Connection failed: ', e));
    }
    return () => {
      currentCompetitorSub?.dispose()
      performanceResultSub?.dispose()
    }
  }, [connection, dispatch]);

  return (
    <>
      {children}
    </>
  );
}
