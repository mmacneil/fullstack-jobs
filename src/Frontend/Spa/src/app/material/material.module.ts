import { MatMenuModule, MatSelectModule, MatOptionModule, MatChipsModule, MatExpansionModule, MatToolbarModule, MatButtonModule, MatInputModule, MatIconModule, MatCardModule, MatProgressSpinnerModule, MatListModule, MatSidenavModule } from '@angular/material';
import { NgModule } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
    imports: [MatMenuModule, MatSelectModule, MatOptionModule, MatChipsModule, MatExpansionModule, DragDropModule, MatToolbarModule, MatButtonModule, MatInputModule, MatIconModule, MatCardModule, MatProgressSpinnerModule, MatListModule, MatSidenavModule],
    exports: [MatMenuModule, MatSelectModule, MatOptionModule, MatChipsModule, MatExpansionModule, DragDropModule, MatToolbarModule, MatButtonModule, MatInputModule, MatIconModule, MatCardModule, MatProgressSpinnerModule, MatListModule, MatSidenavModule]
})

export class MaterialModule { }