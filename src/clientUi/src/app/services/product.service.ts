import { Injectable } from '@angular/core';
import { HttpClient }  from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError,retry } from 'rxjs/operators';
//models
import { Product,ProductDetail } from 'src/app/models/IModels';
import { HttpInterceptorService } from '../services/http-interceptor.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

    private apiUrl ="api/product";

    constructor(
        private http:HttpClient,
        private interceptor: HttpInterceptorService
    ) { }
    
    getUrlContent = () =>  this.apiUrl +"/products";

    // ============ Method ============
    getList(): Observable<Product[]>{
        return this.http
        .get<Product[]>(this.apiUrl)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list data',[]))
        )
    }

    getListById(ids:string): Observable<Product[]>{
        return this.http
        .get<Product[]>(this.apiUrl+"/"+ids)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list by id',[]))
        )
    }

    getListBestSeller(): Observable<Product[]>{
        return this.http
        .get<Product[]>(this.apiUrl+"/best-seller")
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list best seller',[]))
        )
    }

    getListByCate(idCate:number): Observable<Product[]>{
        if(!idCate) return throwError("Category Id is invalid");
        return this.http
        .get<Product[]>(this.apiUrl+"/cate/"+idCate)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list by category',[]))
        )
    }

    getListDiscount(): Observable<Product[]>{
        return this.http
        .get<Product[]>(this.apiUrl+"/promotion")
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list promotion',[]))
        )
    }

    search(query:string): Observable<Product[]>{
        if(!query) return throwError("Query is empty");
        return this.http
        .get<Product[]>(this.apiUrl+"/search/"+query)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list by query',[]))
        )
    }
    // product detail

    get(id:number): Observable<ProductDetail>{
        if(!id && id < 0) return throwError("Id is invalid");
        return this.http
        .get<ProductDetail>(this.apiUrl+"/"+id)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<ProductDetail>('Get product detail '+id,[]))
        )
    }

    add(idCate:number, product:ProductDetail): Observable<ProductDetail>{
        return
    }

    update(id:number, product:ProductDetail): Observable<ProductDetail>{
        return
    }

    updateStatus(id:number, status:number): boolean{
        return
    }

    remove(id:number): boolean{
        return
    }

    // product attribute
    getListAttr(id:string): Observable<Product[]>{
        return this.http
        .get<Product[]>(this.apiUrl+"/attr/"+id)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product[]>('Get list attr',[]))
        )
    }

    getAttr(id:string): Observable<Product>{
        console.log("Get "+id);
        if(!id) return throwError("Query is empty");
        return this.http
        .get<Product>(this.apiUrl+"/attr/"+id)
        .pipe(
            retry(3),
            catchError(this.interceptor.handleError<Product>('Get item attribute',[]))
        )
    }

    addAttr(idCate:number, product:ProductDetail): Observable<ProductDetail>{
        return
    }

    updateAttr(id:number, product:ProductDetail): Observable<ProductDetail>{
        return
    }

    updateStatusAttr(id:number, status:number): boolean{
        return
    }

    removeAttr(id:number): boolean{
        return
    }
}
