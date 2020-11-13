import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApiMessageComponent } from './components/api-message/api-message.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { StudentComponent } from './components/student/student.component';
import { TeacherDetailComponent } from './components/teacher/teacher-detail/teacher-detail.component';
import { TeacherComponent } from './components/teacher/teacher.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'students', component: StudentComponent },
  { path: 'student/:id', component: StudentComponent },
  { path: 'teacher/:id', component: TeacherDetailComponent },
  { path: 'teachers', component: TeacherComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'api-notification', component: ApiMessageComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
