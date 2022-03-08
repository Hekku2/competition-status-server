import { HubConnection, HubConnectionBuilder, IStreamSubscriber, ISubscription } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { CurrentCompetitorEnvelopeModel, PerformanceResultsEnvelopeModel, ScoreboardStatusModel } from "../services/openapi";
import { useAppDispatch } from "../store";
import { setCurrentCompetitor, setLatestResults } from "../store/competition/competitionSlice";
import { setScoreboardStatus } from "../store/scoreboard/scoreboardSlice";

export interface HubConnectionProviderProps {
  baseUrl: string,
  children?: React.ReactNode
}

export const HubConnectionProvider = ({ children, baseUrl }: HubConnectionProviderProps) => {
  const dispatch = useAppDispatch()
  const [competitionHubConnection, setCompetitionHubConnection] = useState<HubConnection | undefined>(undefined);
  const [scoreboardHubConnection, setScoreboardHubConnection] = useState<HubConnection | undefined>(undefined);

  const handler = (handlerMethod: (value: any) => void) => {
    return {
      next: handlerMethod,
      complete: () => { console.log('Finished') },
      error: (error: any) => { console.error(error) }
    }
  }

  useEffect(() => {
    const competitionHub = new HubConnectionBuilder()
      .withUrl(`${baseUrl}/competition-hub`)
      .withAutomaticReconnect()
      .build();
    setCompetitionHubConnection(competitionHub);

    const scoreboardHub = new HubConnectionBuilder()
      .withUrl(`${baseUrl}/scoreboard-hub`)
      .withAutomaticReconnect()
      .build();
    setScoreboardHubConnection(scoreboardHub);

    return () => {
      competitionHub.stop()
      scoreboardHub.stop()
    }
  }, [baseUrl]);

  useEffect(() => {
    const competitorHandler = handler((value: CurrentCompetitorEnvelopeModel) => {
      dispatch(setCurrentCompetitor(value.content))
    })
    const performanceResultsHandler = handler((value: PerformanceResultsEnvelopeModel) => {
      dispatch(setLatestResults(value.content))
    })

    const scoreboardStatusandler: IStreamSubscriber<ScoreboardStatusModel> = {
      next: (value: ScoreboardStatusModel) => {
        dispatch(setScoreboardStatus(value))
      },
      complete: () => { console.log('Finished') },
      error: (error) => { console.error(error) }
    }

    let currentCompetitorSub: ISubscription<CurrentCompetitorEnvelopeModel>;
    let performanceResultSub: ISubscription<PerformanceResultsEnvelopeModel>;
    let scoreboardStatusSub: ISubscription<ScoreboardStatusModel>;
    if (competitionHubConnection) {
      competitionHubConnection.start()
        .then(() => {
          console.log('Connected to competition hub!');

          currentCompetitorSub = competitionHubConnection.stream('StreamCompetitors').subscribe(competitorHandler);
          performanceResultSub = competitionHubConnection.stream('StreamPerformanceResults').subscribe(performanceResultsHandler);
        })
        .catch((e: any) => console.log('Connection failed: ', e));
    }
    if (scoreboardHubConnection) {
      scoreboardHubConnection.start()
        .then(() => {
          console.log('Connected to scoreboard hub!');

          scoreboardStatusSub = scoreboardHubConnection.stream('StreamScoreboardStatus').subscribe(scoreboardStatusandler);
        })
        .catch((e: any) => console.log('Connection failed: ', e));

    }

    return () => {
      currentCompetitorSub?.dispose()
      performanceResultSub?.dispose()
      scoreboardStatusSub?.dispose()
    }
  }, [competitionHubConnection, scoreboardHubConnection, dispatch]);

  return (
    <>
      {children}
    </>
  );
}
