import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { MessageService } from '../services/message.service';



@Injectable({
    providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*', 'Access-Control-Allow-Methods': 'GET' })
    };

    constructor(
        private message: MessageService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var reqAc = req.clone(this.httpOptions);
        console.log("Interceptor HTTP");
        return next.handle(reqAc);
    }

    handleError<T>(operation = 'operation', result?) {
        return (error: any): Observable<T> => {
            let erMsg = error.error.detail;
            if (!erMsg) erMsg =  error.statusText +"\n"+ error.url;
            this.message.showFail(error.status+" - "+ erMsg,operation);
            console.log(error);
            return throwError(result);
        }
    }

    clientError(operation = 'operation',err:string, result? ){
        this.message.showFail(err,operation);
        return throwError(result);
    }
}