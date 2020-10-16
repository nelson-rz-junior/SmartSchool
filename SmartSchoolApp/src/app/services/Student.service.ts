import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Student } from '../models/Student';

@Injectable({
  providedIn: 'root'
})
export class StudentService
{
  baseURL = `${environment.mainUrlApi}/aluno`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseURL);
  }

  getById(id: number): Observable<Student> {
    return this.http.get<Student>(`${this.baseURL}/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.baseURL}/disciplina/${id}`);
  }

  post(student: Student) {
    return this.http.post(this.baseURL, student);
  }

  put(student: Student) {
    return this.http.put(`${this.baseURL}/${student.id}`, student);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
