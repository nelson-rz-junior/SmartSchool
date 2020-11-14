import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginationResult } from '../models/Pagination';
import { Student } from '../models/Student';

@Injectable({
  providedIn: 'root'
})
export class StudentService
{
  baseUrlV1 = `${environment.mainUrlApiV1}/aluno`;
  baseUrlV2 = `${environment.mainUrlApiV2}/aluno`;

  constructor(private http: HttpClient) { }

  getAll(currentPage?: number, itemsPerPage?: number): Observable<PaginationResult<Student[]>> {
    const paginatedResult: PaginationResult<Student[]> = new PaginationResult<Student[]>();

    let params = new HttpParams();
    if (currentPage !== null && itemsPerPage !== null) {
      params = params.append('PageNumber', currentPage.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.get<Student[]>(this.baseUrlV1, { observe: 'response', params})
      .pipe(map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginatedResult;
      }));
  }

  getById(id: number): Observable<Student> {
    return this.http.get<Student>(`${this.baseUrlV1}/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.baseUrlV1}/disciplina/${id}`);
  }

  post(student: Student) {
    return this.http.post(this.baseUrlV1, student);
  }

  put(student: Student) {
    return this.http.put(`${this.baseUrlV1}/${student.id}`, student);
  }

  patch(student: Student) {
    return this.http.patch(`${this.baseUrlV2}/${student.id}`, student);
  }

  changeStatus(student: Student) {
    return this.http.patch(`${this.baseUrlV2}/${student.id}/changeStatus`, { status: student.ativo });
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrlV1}/${id}`);
  }
}
